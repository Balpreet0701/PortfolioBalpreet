import React, { useEffect, useMemo, useState } from 'react';
import { fallbackPortfolio } from './data/fallbackPortfolio.js';

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5042';

function App() {
  const [portfolio, setPortfolio] = useState(fallbackPortfolio);
  const [formState, setFormState] = useState({ status: 'idle', message: '' });

  useEffect(() => {
    const controller = new AbortController();

    fetch(`${apiBaseUrl}/api/portfolio`, { signal: controller.signal })
      .then((response) => {
        if (!response.ok) {
          throw new Error('Unable to load portfolio from API.');
        }

        return response.json();
      })
      .then((data) => setPortfolio(data))
      .catch((error) => {
        if (error.name !== 'AbortError') {
          setPortfolio(fallbackPortfolio);
        }
      });

    return () => controller.abort();
  }, []);

  const stats = useMemo(
    () => [
      { value: '2+', label: 'Roles at Persistent' },
      { value: '3', label: 'Major Projects' },
      { value: '4', label: 'Cloud Certifications' },
      { value: '9.94', label: 'B.Tech CGPA' },
    ],
    []
  );

  async function handleContactSubmit(event) {
    event.preventDefault();
    const form = event.currentTarget;
    const formData = new FormData(form);

    const payload = {
      name: formData.get('name'),
      email: formData.get('email'),
      subject: formData.get('subject'),
      message: formData.get('message'),
    };

    setFormState({ status: 'sending', message: 'Sending...' });

    try {
      const response = await fetch(`${apiBaseUrl}/api/contact`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        throw new Error('Please check the form and try again.');
      }

      form.reset();
      setFormState({ status: 'success', message: 'Thanks, your message was saved.' });
    } catch (error) {
      setFormState({
        status: 'error',
        message: 'Start the .NET API, then send the message again.',
      });
    }
  }

  return (
    <main>
      <Hero profile={portfolio.profile} stats={stats} />
      <section className="section section--intro" id="about">
        <div className="container intro-grid">
          <div>
            <p className="section-kicker">About</p>
            <h2>Building practical web systems with clear APIs and reliable data.</h2>
          </div>
          <p className="intro-copy">{portfolio.profile.summary}</p>
        </div>
      </section>

      <section className="section" id="experience">
        <div className="container">
          <SectionHeading eyebrow="Experience" title="Persistent Systems" />
          <div className="timeline">
            {portfolio.experiences.map((experience) => (
              <article className="timeline-item" key={`${experience.role}-${experience.startDate}`}>
                <div className="timeline-marker" aria-hidden="true" />
                <div className="timeline-content">
                  <div className="item-heading">
                    <div>
                      <h3>{experience.role}</h3>
                      <p>{experience.company}</p>
                    </div>
                    <span>
                      {experience.startDate} - {experience.endDate}
                    </span>
                  </div>
                  <p className="muted">{experience.location}</p>
                  <ul className="clean-list">
                    {experience.highlights.map((highlight) => (
                      <li key={highlight}>{highlight}</li>
                    ))}
                  </ul>
                </div>
              </article>
            ))}
          </div>
        </div>
      </section>

      <section className="section section--tinted" id="projects">
        <div className="container">
          <SectionHeading eyebrow="Projects" title="Selected work" />
          <div className="project-grid">
            {portfolio.projects.map((project) => (
              <article className="project-card" key={project.name}>
                <div className="project-card__top">
                  <span>{project.dateRange}</span>
                  <h3>{project.name}</h3>
                  <p>{project.summary}</p>
                </div>
                <div className="chip-row">
                  {project.technologies.map((technology) => (
                    <span className="chip" key={technology}>
                      {technology}
                    </span>
                  ))}
                </div>
                <ul className="clean-list">
                  {project.highlights.map((highlight) => (
                    <li key={highlight}>{highlight}</li>
                  ))}
                </ul>
              </article>
            ))}
          </div>
        </div>
      </section>

      <section className="section" id="skills">
        <div className="container skills-layout">
          <div>
            <SectionHeading eyebrow="Skills" title="Stack and fundamentals" />
            <div className="skills-grid">
              {portfolio.skillCategories.map((category) => (
                <article className="skill-card" key={category.name}>
                  <h3>{category.name}</h3>
                  <div className="chip-row">
                    {category.skills.map((skill) => (
                      <span className="chip chip--light" key={skill}>
                        {skill}
                      </span>
                    ))}
                  </div>
                </article>
              ))}
            </div>
          </div>
          <aside className="subject-panel">
            <h3>Core Subjects</h3>
            <ul>
              {portfolio.coreSubjects.map((subject) => (
                <li key={subject}>{subject}</li>
              ))}
            </ul>
          </aside>
        </div>
      </section>

      <section className="section section--split" id="education">
        <div className="container split-grid">
          <div>
            <SectionHeading eyebrow="Education" title="Academic record" />
            <div className="stack-list">
              {portfolio.education.map((item) => (
                <article className="stack-card" key={`${item.degree}-${item.dateRange}`}>
                  <div>
                    <h3>{item.degree}</h3>
                    <p>{item.institution}</p>
                    <span>{item.location}</span>
                  </div>
                  <strong>{item.result}</strong>
                  <span>{item.dateRange}</span>
                </article>
              ))}
            </div>
          </div>
          <div>
            <SectionHeading eyebrow="Achievements" title="Certifications" />
            <div className="cert-list">
              {portfolio.certifications.map((certification) => (
                <div className="cert-item" key={certification}>
                  {certification}
                </div>
              ))}
            </div>
          </div>
        </div>
      </section>

      <section className="section contact-section" id="contact">
        <div className="container contact-grid">
          <div>
            <p className="section-kicker">Contact</p>
            <h2>Let us build something reliable and useful.</h2>
            <div className="contact-links">
              <a href={`mailto:${portfolio.profile.email}`}>{portfolio.profile.email}</a>
              <a href={portfolio.profile.linkedInUrl} target="_blank" rel="noreferrer">
                LinkedIn
              </a>
              <a href={portfolio.profile.gitHubUrl} target="_blank" rel="noreferrer">
                GitHub
              </a>
            </div>
          </div>
          <form className="contact-form" onSubmit={handleContactSubmit}>
            <label>
              Name
              <input name="name" type="text" placeholder="Your name" required />
            </label>
            <label>
              Email
              <input name="email" type="email" placeholder="you@example.com" required />
            </label>
            <label>
              Subject
              <input name="subject" type="text" placeholder="Project or role" />
            </label>
            <label>
              Message
              <textarea name="message" rows="5" placeholder="Write your message" required />
            </label>
            <button type="submit" disabled={formState.status === 'sending'}>
              {formState.status === 'sending' ? 'Sending' : 'Send Message'}
            </button>
            {formState.message && <p className={`form-message form-message--${formState.status}`}>{formState.message}</p>}
          </form>
        </div>
      </section>
    </main>
  );
}

function Hero({ profile, stats }) {
  return (
    <header className="hero">
      <nav className="nav" aria-label="Main navigation">
        <a className="brand" href="#top">
          BK
        </a>
        <div>
          <a href="#experience">Experience</a>
          <a href="#projects">Projects</a>
          <a href="#skills">Skills</a>
          <a href="#contact">Contact</a>
        </div>
      </nav>
      <div className="hero-content" id="top">
        <p className="eyebrow">{profile.title}</p>
        <h1>{profile.name}</h1>
        <p>{profile.summary}</p>
        <div className="hero-actions">
          <a className="button button--primary" href="#projects">
            View Projects
          </a>
          <a className="button button--ghost" href={`mailto:${profile.email}`}>
            Email Me
          </a>
        </div>
        <dl className="stats-grid">
          {stats.map((stat) => (
            <div key={stat.label}>
              <dt>{stat.value}</dt>
              <dd>{stat.label}</dd>
            </div>
          ))}
        </dl>
      </div>
    </header>
  );
}

function SectionHeading({ eyebrow, title }) {
  return (
    <div className="section-heading">
      <p className="section-kicker">{eyebrow}</p>
      <h2>{title}</h2>
    </div>
  );
}

export default App;
